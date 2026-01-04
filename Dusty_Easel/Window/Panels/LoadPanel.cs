using System.Numerics;
using Dusty_Easel.Panels;
using Editor.Panels;
using ImGuiNET;
using NativeFileDialogSharp;

namespace Dusty_Easel.Window.Panels;

public class LoadPanel : IRenderablePanel
{
    public bool ShowPanel;
    private string _path = "";

    public void Render()
    {
        if (!ShowPanel) return;

        ImGui.SetNextWindowSize(new Vector2(500, 300), ImGuiCond.FirstUseEver);
        ImGui.SetNextWindowPos(new Vector2(200, 150), ImGuiCond.FirstUseEver);

        ImGui.Begin("Load Image", ref ShowPanel,
            ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoCollapse);
        
        ImGui.Text("Path: ");
        ImGui.SameLine();
        ImGui.InputText("##name", ref _path, 512);
        ImGui.SameLine();

        if (ImGui.Button("Browse..."))
            OpenLoadFileDialog();
        
        ImGui.Spacing();

        if (ImGui.Button("Load", new Vector2(120, 0)) && _path != null)
        {
            SaveLoadImage.LoadPngToBytes(_path, out byte[]? rgba, out int width, out int height);
            PixelRenderer.PixelData = rgba;
        }

        ImGui.End();
    }
    
    private void OpenLoadFileDialog()
    {
        try
        {
            var defaultPath = string.IsNullOrEmpty(_path) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : _path;

            var result = Dialog.FileOpen("png", defaultPath);

            if (!result.IsOk) return;

            var selectedPath = result.Path;

            if (!selectedPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Please select a PNG file");
                return;
            }

            if (!File.Exists(selectedPath))
            {
                Console.WriteLine("File does not exist");
                return;
            }

            _path = selectedPath;
        
            Console.WriteLine($"Selected file: {_path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Dialog error: {ex.Message}");
        }
    }
}