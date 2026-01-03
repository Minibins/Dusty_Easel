using System.Numerics;
using Dusty_Easel;
using Dusty_Easel.Panels;
using ImGuiNET;

namespace Editor.Panels;

public enum DrawTool
{
    Pencil,
    Line,
    Circle,
    Rectangle,
    Fill
}


public class SettingPanel(PixelRenderer renderer) : IRenderablePanel
{
    public static Vector4 BrushColor = new(0, 0, 0, 1);
    public static int BrushSize = 1;
    
    public static DrawTool CurrentTool = DrawTool.Pencil;
    public void Render()
    {
        ImGui.Begin("Settings");
        
        RenderToolbar();
        ImGui.Separator();
        RenderCanvasSettings();
        
        ImGui.End();
    }
    
    private void RenderToolbar()
    {
        ImGui.Text("Tool:");
        ImGui.SameLine();
        
        if (ImGui.RadioButton("Pencil", CurrentTool == DrawTool.Pencil))
            CurrentTool = DrawTool.Pencil;
        
        ImGui.ColorEdit4("Color", ref BrushColor);
        
        if (CurrentTool == DrawTool.Pencil)
            ImGui.SliderInt("Brush Size", ref BrushSize, 1, 10);

        if (ImGui.Button("Clear Canvas"))
            renderer?.ClearToColor(new OpenTK.Mathematics.Vector4(1, 1, 1, 1));
    }
    
    private void RenderCanvasSettings()
    {
        if (!ImGui.CollapsingHeader("Canvas Settings")) return;
        
        ImGui.InputInt("Width", ref PixelCanvasPanel.CanvasWidth);
        ImGui.InputInt("Height", ref PixelCanvasPanel.CanvasHeight);
            
        PixelCanvasPanel.CanvasWidth = Math.Clamp(PixelCanvasPanel.CanvasWidth, 8, 256);
        PixelCanvasPanel.CanvasHeight = Math.Clamp(PixelCanvasPanel.CanvasHeight, 8, 256);
            
        if (ImGui.Button("Resize Canvas"))
            renderer.Resize(PixelCanvasPanel.CanvasWidth, PixelCanvasPanel.CanvasHeight);
            
        ImGui.SliderInt("Zoom", ref PixelCanvasPanel.PixelScale, 1, 32);
        ImGui.Checkbox("Show Grid", ref PixelCanvasPanel.ShowGrid);
    }
}