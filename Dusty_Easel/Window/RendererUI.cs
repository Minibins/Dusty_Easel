using Dusty_Easel.Panels;
using Dusty_Easel.Window.Panels;
using Editor.Panels;
using ImGuiNET;
using OpenTK.Windowing.Desktop;

namespace Dusty_Easel;

public class RendererUI
{
    private static ExportPanel? _exportPanel;
    private static LoadPanel? _loadPanel;
    private readonly List<IRenderablePanel> _renderablePanels;

    public RendererUI(PixelRenderer renderer)
    {
        _exportPanel = new ExportPanel();
        _loadPanel = new LoadPanel();
        _renderablePanels =
        [
            new PixelCanvasPanel(renderer),

            new SettingPanel(renderer),
            _exportPanel,
            _loadPanel
        ];
    }

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
            if (ImGui.MenuItem("Save"))
                _exportPanel!.ShowPanel = !_exportPanel.ShowPanel;
            
            if (ImGui.MenuItem("Load", ""))
                _loadPanel!.ShowPanel = !_loadPanel.ShowPanel;

            ImGui.Separator();

            if (ImGui.MenuItem("Exit"))
                window.Close();

            ImGui.EndMenu();
        }

        ImGui.EndMainMenuBar();
    }
}