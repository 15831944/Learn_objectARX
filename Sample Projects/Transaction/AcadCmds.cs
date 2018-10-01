﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.AutoCAD.Runtime;
using AcadApp = Autodesk.AutoCAD.ApplicationServices;
using AcadGeo = Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Windows;

[assembly: CommandClass(typeof(TransactionARX.AcadCmds))]
namespace TransactionARX
{
	public class AcadCmds
	{
		[CommandMethod("TestTransaction")]
		public void DrawLine1()
		{
			Database db = AcadApp.Application.DocumentManager.MdiActiveDocument.Database;

			using (Transaction tr = db.TransactionManager.StartTransaction())
			{
				BlockTable blk_tbl = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
				if (null == blk_tbl)
					return;

				BlockTableRecord model_space = tr.GetObject(blk_tbl[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;
				if (null == model_space)
					return;

				Line line = new Line(new AcadGeo.Point3d(0.0, 0.0, 0.0), new AcadGeo.Point3d(1.0, 1.0, 0.0));

				AcadFuncs.AddNewEnt(line);

				tr.Dispose();
			}
		}
	}
}
