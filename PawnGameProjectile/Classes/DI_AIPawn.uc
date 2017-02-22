class DI_AIPawn extends Pawn
 placeable;

var(Pawn) const DynamicLightEnvironmentComponent LightEnvironment;

var(Pawn) const float UserGroundSpeed<DisplayName=Ground Speed>;

var(Pawn) const ParticleSystem ExplosionParticleTemplate;

var(Pawn) const SoundCue ExplosionSoundCue;

var(Pawn) const int ExplosionDamage;

var(Pawn) const archetype DroppedPickup ArchetypedPickup;

var(Pawn) const float ChanceToDropPickup;

var Actor Enemy;




simulated function PostBeginPlay()
	{
		Super.PostBeginPlay();
		SpawnDefaultController();
	}



simulated function Tick(float DeltaTime)
{
    local PlayerController PlayerController;
    local Vector Direction;
    local Rotator NewRotation;

    Super.Tick(DeltaTime);

    if (Enemy == None)
    {
		// Search the player controller in the world
        PlayerController = GetALocalPlayerController();

        if (PlayerController != None && PlayerController.Pawn != None)
        {
      
            Enemy = PlayerController.Pawn;
        }
    }
	// The Enemy is walking
    else if (Physics == PHYS_Walking)
    {
        // Obtain the direction in order to look at the enemy
        Direction = Enemy.Location - Location;

        NewRotation = Rotator(Direction);
        NewRotation.Pitch = 0;
        NewRotation.Roll = 0;

		// Set the rotation to look at the enemy
        SetRotation(NewRotation);

		//// Set my velocity, in order to move towards the enemy
        Velocity = Normal(Enemy.Location - Location) * UserGroundSpeed;

		// Set the acceleration, to move towards the enemy
        Acceleration = Velocity;
    }
}


event Bump(Actor Other, PrimitiveComponent OtherComp, Vector HitNormal)
{
    Super.Bump(Other, OtherComp, HitNormal);

    if (DIPawn (Other) !=None) 
    {
        // Apply damage to the bumped pawn
        Other.TakeDamage(ExplosionDamage, None, Location, Vect(0, 0, 0), class'DamageType');

        if (ExplosionParticleTemplate != None)
        {
            WorldInfo.MyEmitterPool.SpawnEmitter(ExplosionParticleTemplate, Location);
        }
		// Play the explosion sound
        if (ExplosionSoundCue != None)
        {
            PlaySound(ExplosionSoundCue);
        }
		//Destroy
        Destroy();
    }
}


simulated function PlayDying(class<DamageType> DamageType, vector HitLoc)
{
    local DroppedPickup DroppedPickup;

    Mesh.MinDistFactorForKinematicUpdate = 0.0;
    Mesh.ForceSkelUpdate();
    Mesh.SetTickGroup(TG_PostAsyncWork);
    CollisionComponent = Mesh;
    CylinderComponent.SetActorCollision(false, false);
    Mesh.SetActorCollision(true, false);
    Mesh.SetTraceBlocking(true, true);
	
    //SetPawnRBChannels(true);

    SetPhysics(PHYS_RigidBody);

    Mesh.PhysicsWeight = 1.f;

    if (Mesh.bNotUpdatingKinematicDueToDistance)
    {
        Mesh.UpdateRBBonesFromSpaceBases(true, true);
    }

    Mesh.PhysicsAssetInstance.SetAllBodiesFixed(false);
    Mesh.bUpdateKinematicBonesFromAnimation = false;
    Mesh.WakeRigidBody();

   
    LifeSpan = 10.f;

    
    if (ArchetypedPickup != None && FRand() <= ChanceToDropPickup)
    {
        
        DroppedPickup = Spawn(ArchetypedPickup.Class,,, Location,, ArchetypedPickup);

        if (DroppedPickup != None)
        {
            
            DroppedPickup.SetPhysics(PHYS_Falling);

            
            DroppedPickup.Velocity.X = 0;
            DroppedPickup.Velocity.Y = 0;
            DroppedPickup.Velocity.Z = RandRange(200.f, 250.f);
        }
    }
}



DefaultProperties
{
		// Ground speed of the pawn
		UserGroundSpeed=150.f
		// Set physics to falling
		Physics=PHYS_Falling
		// Remove the sprite component as it is not needed
		Components.Remove(Sprite)
		// Create a light environment for the pawn
		Begin Object Class=DynamicLightEnvironmentComponent Name=MyLightEnvironment
		bSynthesizeSHLight=true
		bIsCharacterLightEnvironment=true
		bUseBooleanEnvironmentShadowing=false
		End Object
		Components.Add(MyLightEnvironment)
		LightEnvironment=MyLightEnvironment
		// Create a skeletal mesh component for the pawn
		Begin Object Class=SkeletalMeshComponent Name=MySkeletalMeshComponent
		bCacheAnimSequenceNodes=false
		AlwaysLoadOnClient=true
		AlwaysLoadOnServer=true
		CastShadow=true
		BlockRigidBody=true
		bUpdateSkelWhenNotRendered=false
		bIgnoreControllersWhenNotRendered=true
		bUpdateKinematicBonesFromAnimation=true
		bCastDynamicShadow=true
		RBChannel=RBCC_Untitled3
		RBCollideWithChannels=(Untitled3=true)
		LightEnvironment=MyLightEnvironment
		bOverrideAttachmentOwnerVisibility=true
		bAcceptsDynamicDecals=false
		bHasPhysicsAssetInstance=true
		TickGroup=TG_PreAsyncWork
		MinDistFactorForKinematicUpdate=0.2f
		bChartDistanceFactor=true
		RBDominanceGroup=20
		Scale=1.f
		bAllowAmbientOcclusion=false
		bUseOnePassLightingOnTranslucency=true
		bPerBoneMotionBlur=true
		End Object
		Mesh=MySkeletalMeshComponent
		Components.Add(MySkeletalMeshComponent)

}