

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
 *  Copyright 2023 The TweetyProject Team <http://tweetyproject.org/contact/>
 */
namespace TweetyProject.NET.Commons;

/// <summary>
/// This class is responsible for the behavior of a plotter with multiple frames. 
/// Closing one frame shall only exit the application, iff it was the last frame.
/// 
/// @author Julian Sander
/// @version TweetyProject 1.23
/// </summary>
public class PlotterMultiFrame : Plotter
{
    private static int _SOpenedFrames = 0;

    /// <summary>
    /// {@inheritDoc}
    /// </summary>
    public override void CreateFrame(int FrameWidth, int FrameHeight)
    {
        base.CreateFrame(FrameWidth, FrameHeight);


        this.Frame.AddWindowListener(new WindowListenerAnonymousInnerClass(this));
    }

    private class WindowListenerAnonymousInnerClass : WindowListener
    {
        private readonly PlotterMultiFrame _OuterInstance;

        public WindowListenerAnonymousInnerClass(PlotterMultiFrame OuterInstance)
        {
            this._OuterInstance = OuterInstance;
        }


        public override void windowOpened(WindowEvent E)
        {
        }

        public override void windowIconified(WindowEvent E)
        {
        }

        public override void windowDeiconified(WindowEvent E)
        {
        }

        public override void windowDeactivated(WindowEvent E)
        {
        }

        public override void windowClosing(WindowEvent E)
        {
        }

        public override void windowClosed(WindowEvent E)
        {
            _SOpenedFrames--;
            if (_SOpenedFrames == 0)
            {
                Environment.Exit(0);
            }
        }

        public override void windowActivated(WindowEvent E)
        {
        }
    }

    public override void Show()
    {
        base.Show();
        _SOpenedFrames++;
        this.Frame.SetDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
    }
}