using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standartTurret;
    public TurretBlueprint missileLauncherTurret;
    public TurretBlueprint laserBeamerTurret;
    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        Debug.Log("Tourelle standard selectionnees");
        buildManager.SelectTurretToBuild(standartTurret);
    }
    public void SelectMissileLauncher()
    {
        Debug.Log("Lance Missile selectionne");
        buildManager.SelectTurretToBuild(missileLauncherTurret);
    }
    public void SelectLaserBeamer()
    {
        Debug.Log("Laser Beamer selectionne");
        buildManager.SelectTurretToBuild(laserBeamerTurret);
    }
}
