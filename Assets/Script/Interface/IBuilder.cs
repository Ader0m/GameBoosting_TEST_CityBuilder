using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal interface IBuilder
{
	public void BuildTownCell();
	public void BuildGrassCell();
	public void BuildSendCell();
	public void BuildSwampCell();
	public void BuildWaterCell();
}
