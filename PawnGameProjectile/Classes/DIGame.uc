// Dark Intent Main Class
class DIGame extends UTGame;

DefaultProperties
{
	MapPrefixes[0]="DI"
    DefaultPawnClass=class'DarkIntent.DIPawn'
	DefaultPawnArchetype=DIPawn'StarterPlatformGameContent.Archetypes.PlayerPawn'
    PlayerControllerClass=class'DarkIntent.DIPlayerController'
	HUDType=class'DIHUD'
	bUseClassicHUD=true
	DefaultInventory(0)=class'DIShockRifle'
}
