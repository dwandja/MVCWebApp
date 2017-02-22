class DIPlayerController extends UTPlayerController;

// regular player walking
state PlayerWalking
{
ignores SeePlayer, HearNoise, Bump;

   function ProcessMove(float DeltaTime, vector NewAccel, eDoubleClickDir DoubleClickMove, rotator DeltaRot)
   {

      local Rotator tempRot;
      if( Pawn == None ) return;
      if (Role == ROLE_Authority) Pawn.SetRemoteViewPitch( Rotation.Pitch );

      Pawn.Acceleration.X = PlayerInput.aForward * DeltaTime * 100 * PlayerInput.MoveForwardSpeed; // was aStrafe
      Pawn.Acceleration.Y = 0;
      Pawn.Acceleration.Z = 0;
      
      tempRot.Pitch = Pawn.Rotation.Pitch;
      tempRot.Roll = 0;

      if(Normal(Pawn.Acceleration) Dot Vect(1,0,0) > 0)
      {
         tempRot.Yaw = 0;
         Pawn.SetRotation(tempRot);
      } else if(Normal(Pawn.Acceleration) Dot Vect(1,0,0) < 0)
      {
         tempRot.Yaw = 32768;
         Pawn.SetRotation(tempRot);
      }

      CheckJumpOrDuck();

   }
}

// used for ghost/fly modes
state PlayerFlying
{
ignores SeePlayer, HearNoise, Bump;

	function PlayerMove(float DeltaTime)
	{

		local Rotator tempRot;
		if( Pawn == None ) return;
		if (Role == ROLE_Authority) Pawn.SetRemoteViewPitch( Rotation.Pitch );

		Pawn.Acceleration.X = PlayerInput.aForward; // was aStrafe
		Pawn.Acceleration.Y = 0;
		Pawn.Acceleration.Z = PlayerInput.aUp;

		Pawn.Acceleration = Pawn.AccelRate * Normal(Pawn.Acceleration);

		if ( bCheatFlying && (Pawn.Acceleration == vect(0,0,0)) )
			Pawn.Velocity = vect(0,0,0);
		// Update rotation.
		UpdateRotation( DeltaTime );
      
		tempRot.Pitch = Pawn.Rotation.Pitch;
		tempRot.Roll = 0;

		if(Normal(Pawn.Acceleration) Dot Vect(1,0,0) > 0)
		{
			tempRot.Yaw = 0;
			Pawn.SetRotation(tempRot);
		} else if(Normal(Pawn.Acceleration) Dot Vect(1,0,0) < 0)
		{
			tempRot.Yaw = 32768;
			Pawn.SetRotation(tempRot);
		}

		CheckJumpOrDuck();

	}

	event BeginState(Name PreviousStateName)
	{
		Pawn.SetPhysics(PHYS_Flying);
	}
}

function UpdateRotation( float DeltaTime )
{
   local Rotator   DeltaRot, ViewRotation;
   ViewRotation = Rotation;

   // Calculate Delta to be applied on ViewRotation
   DeltaRot.Yaw = Pawn.Rotation.Yaw;
   DeltaRot.Pitch   = PlayerInput.aLookUp;
   ProcessViewRotation( DeltaTime, ViewRotation, DeltaRot );
   SetRotation(ViewRotation);
}   

defaultproperties
{
	
}