using System.Numerics;
using Dusty_Easel.Panels;
using Editor.Panels;
using ImGuiNET;
using NativeFileDialogSharp;

namespace Dusty_Easel.Window.Panels;

public class ExportPanel : IRenderablePanel
{
    public bool ShowPanel;
    private string _path = "";
    private string _filename = "image.png";

    public void Render()
    {
        if (!ShowPanel) return;

        ImGui.SetNextWindowSize(new Vector2(500, 300), ImGuiCond.FirstUseEver);
        ImGui.SetNextWindowPos(new Vector2(200, 150), ImGuiCond.FirstUseEver);

        ImGui.Begin("Export Image", ref ShowPanel,
            ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoCollapse);

        ImGui.Text("Filename: ");
        ImGui.SameLine();
        ImGui.InputText("##name", ref _filename, 256);

        ImGui.Text("Path: ");
        ImGui.SameLine();
        ImGui.InputText("##name", ref _path, 512);
        ImGui.SameLine();

        if (ImGui.Button("Browse..."))
            OpenSaveFileDialog();

        ImGui.Spacing();

        var fullPath = GetFullPath();
        ImGui.TextWrapped($"Full path: {fullPath}");

        ImGui.Spacing();

        if (ImGui.Button("Export", new Vector2(120, 0)))
        {
            try
            {
                var directory = Path.GetDirectoryName(fullPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                SaveLoadImage.SaveRgbaToPng(
                    PixelRenderer.PixelData!,
                    PixelCanvasPanel.CanvasWidth,
                    PixelCanvasPanel.CanvasHeight,
                    fullPath);

                Console.WriteLine($"Image exported to: {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Export failed: {ex.Message}");
            }
        }

        ImGui.End();
    }

    private string GetFullPath()
    {
        var filename = _filename;
        if (!filename.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            filename += ".png";

        return string.IsNullOrEmpty(_path) ? filename : Path.Combine(_path, filename);
    }

    private void OpenSaveFileDialog()
    {
        try
        {
            var filenameWithExt = _filename;
            if (!filenameWithExt.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                filenameWithExt += ".png";

            var defaultPath = string.IsNullOrEmpty(_path)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), filenameWithExt)
                : Path.Combine(_path, filenameWithExt);

            var result = Dialog.FileSave("png", defaultPath);

            if (!result.IsOk) return;

            var selectedPath = result.Path;

            if (!selectedPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                selectedPath += ".png";

            _path = Path.GetDirectoryName(selectedPath) ?? "";
            _filename = Path.GetFileName(selectedPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Dialog error: {ex.Message}");
        }
    }
}