

/*
 *  This file is part of "TweetyProject", a collection of Java libraries for
 *  logical aspects of artificial intelligence and knowledge representation.
 *
 *  TweetyProject is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License version 3 as
 *  published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 *  Copyright 2021 The TweetyProject Team <http://tweetyproject.org/contact/>
 */
namespace TweetyProject.NET.Commons;

/// <summary>
/// A class for managing and displaying several plots in a single frame
/// @author Benedikt Knopp
/// </summary>
public class Plotter
{

    /// <summary>
    /// The frame where the model is drawn
    /// </summary>
    protected internal JFrame Frame;
    /// <summary>
    /// The main panel in the frame
    /// </summary>
    private JPanel _MainPanel;

    /// <summary>
    /// Create a new instance
    /// </summary>
    public Plotter()
    {
    }

    /// <summary>
    /// Create a new main frame with specific proportions </summary>
    /// <param name="frameWidth"> the width of the frame </param>
    /// <param name="frameHeight"> the height of the frame </param>
    public virtual void CreateFrame(int FrameWidth, int FrameHeight)
    {
        this.Frame = new JFrame();
        this._MainPanel = new JPanel(new FlowLayout());
        this._MainPanel.SetPreferredSize(new Dimension(FrameWidth, FrameHeight));
        this.Frame.SetSize(FrameWidth, FrameHeight);

    }

    /// <summary>
    /// Show the frame after adding some plots
    /// </summary>
    public virtual void Show()
    {
        JScrollPane ScrollPane = new JScrollPane(_MainPanel, JScrollPane.VERTICAL_SCROLLBAR_ALWAYS, JScrollPane.HORIZONTAL_SCROLLBAR_ALWAYS);
        this.Frame.Add(ScrollPane);
        this.Frame.SetVisible(true);
        this.Frame.SetDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }

    /// <returns> the frame </returns>
    public virtual JFrame Frame
    {
        get
        {
            return Frame;
        }
    }

    /// <summary>
    /// Add a sub-plot the the frame </summary>
    /// <param name="panel"> the panel containing the sub-plot </param>
    public virtual void Add(JPanel Panel)
    {
        this._MainPanel.Add(Panel);
    }

    /// <summary>
    /// Get the horizontal gap between panels </summary>
    /// <returns> the horizontal gap </returns>
    public virtual int HGap
    {
        get
        {
            return 20;
        }
    }

    /// <summary>
    /// Get the vertical gap between panels </summary>
    /// <returns> the vertical gap </returns>
    public virtual int VGap
    {
        get
        {
            return 20;
        }
    }

    /// <summary>
    /// Add some description to the frame </summary>
    /// <param name="labels"> a list of strings that will be aligned vertically </param>
    public virtual void AddLabels(IList<string> Labels)
    {
        string LabelHTML = "<html>";
        foreach (string Label in Labels)
        {
            LabelHTML += Label + "<br>";
        }
        LabelHTML += "</html>";
        this._MainPanel.Add(new JLabel(LabelHTML), FlowLayout.LEFT);
    }


}