using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models.ChartJs
{
    public partial class legend
    {
        // Label that will be displayed
        String text { get; set; }

        // Fill style of the legend box color
        string fillStyle { get; set; }

        // If true, this item represents a hidden dataset. Label will be rendered with a strike-through effect
        bool hidden { get; set; }

        // For box border. See https://developer.mozilla.org/en/docs/Web/API/CanvasRenderingContext2D/lineCap
        string lineCap { get; set; }

        // For box border. See https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/setLineDash
        decimal[] lineDash { get; set; }

        // For box border. See https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/lineDashOffset
        decimal lineDashOffset { get; set; }

        // For box border. See https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/lineJoin
        string lineJoin { get; set; }

        // Width of box border
        decimal lineWidth { get; set; }

        // Stroke style of the legend box Color
        string strokeStyle { get; set; }

        // Point style of the legend box (only used if usePointStyle is true)
        String pointStyle { get; set; }

    }
}