class DIHUD extends HUD;

/*
 * Measures the Actor Health
 * 
 */

	event PostRender()
		{
		local float right, left;
		local String mes;
		Super.PostRender();
		
		if (PlayerOwner != None && PlayerOwner.Pawn != None)
		{
		
		mes = "Health: "$PlayerOwner.Pawn.Health;
		
		Canvas.Font = class'Engine'.static.GetMediumFont();
		
		Canvas.SetDrawColor(255, 255, 255);
		
		Canvas.StrLen(mes, right, left);
		
		Canvas.SetPos(4, Canvas.ClipY - left - 4);
		
		Canvas.DrawText(mes);
		}
}



	

DefaultProperties
{
}
