

using System.Drawing;
using System.Drawing.Imaging;
using Dusty_Easel.Panels;
using Editor.Panels;
using ImGuiNET;
using OpenTK.Windowing.Desktop;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace Dusty_Easel;

public class RendererUI(PixelRenderer renderer)
{
    private readonly List<IRenderablePanel> _renderablePanels =
    [
        new PixelCanvasPanel(renderer),
        new SettingPanel(renderer)
    ];

    public void Render(GameWindow window)
    {
        RenderTopMenuBar(window);

        ImGui.DockSpaceOverViewport();

        foreach (var panel in _renderablePanels)
            panel.Render();
    }

    private static void RenderTopMenuBar(GameWindow window)
    {
        if (!ImGui.BeginMainMenuBar())
            return;
        
        if (ImGui.BeginMenu("File"))
        {

  
            
            if (ImGui.MenuItem("Save", "Ctrl+S"))
            {
            
            }
            
            if (ImGui.MenuItem("Save As", "Ctrl+Shift+S"))
            {
                SaveRgbaToPng(
                    PixelRenderer.PixelData,
                    PixelCanvasPanel.CanvasWidth,
                    PixelCanvasPanel.CanvasHeight, "/home/maksym/Projects/Dusty_Easel/Dusty_Easel/test.png"
                );
            }

            
            ImGui.Separator();
            
            if (ImGui.MenuItem("Exit"))
            {
                window.Close();
            }
            
            ImGui.EndMenu();
        }
        
        ImGui.EndMainMenuBar();
    }


    private static void SaveRgbaToPng(byte[] rgba, int width, int height, string path)
    {
        if (rgba.Length < width * height * 4)
            throw new ArgumentException("rgba buffer is too small");

        using var img = Image.LoadPixelData<Rgba32>(rgba, width, height);

        img.Save(path, new PngEncoder());
    }

}