﻿using System.Collections.Generic;

namespace ShtatRaspisanie
{
    internal class Unit
    {
        //Наименование подразделения.
        public string name { get; set; }

        //Родительское подразделение
        public string parent { get; set; }

        //Потомок родителя.
        public List<Unit> child { get; set; }
    }
}