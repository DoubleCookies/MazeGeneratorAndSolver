﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator.MazeSolverServices
{
    public class FoundPointsData
    {
        public FoundPointsData(Point point, PointDirections direction) {
            this.direction = direction;
            this.point = point;
        }

        private PointDirections direction;
        private Point point;

        public PointDirections Direction { get => direction; set => direction = value; }
        public Point Point { get => point; set => point = value; }
    }
}
