using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Color startColor;
    private Renderer rend;
    private BuildManager buildManager;

    

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    //une fonction pour ameliorer les tourelles
    public void UpgradeTurret()
    {
        if (PlayerStats.money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Pas assez d'argent pour ameliorer la tourelle.");
            return;
        }

        PlayerStats.money -= turretBlueprint.upgradeCost;

        //supression de l'ancienne tourelle
        Destroy(turret);


        //creation de la nouvelle tourelle amelioree.
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 1f);

        isUpgraded = true;

        Debug.Log("Tourelle amelioree");
    }
    //une fonction pour construire les tourelles
    private void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.money < blueprint.cost)
        {
            Debug.Log("Pas assez d'argent pour cela");
            return;
        }

        PlayerStats.money -= blueprint.cost;

        turretBlueprint = blueprint;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 1f);

        Debug.Log("La tourelle a ete construite.");
    }

    //une fonction pour vendre la tourelle
    public void SellTurret()
    {

        //spawn effet.
        PlayerStats.money += turretBlueprint.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 1f);

        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;
    }

    private void OnMouseDown()
    {
        //si la souris sorte du ui
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //si latourelle estoquee est null
        if (turret != null)
        {
            //Debug.Log("Impossible de construire içi, il y a deja une tourelle.");
            buildManager.SelectNode(this);
            return;
        }
        if (!buildManager.canBuild)
        {
            return;
        }

        //Construction d'une tourelle
        BuildTurret(buildManager.GetTurretToBuild());
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!buildManager.canBuild)
        {
            return;
        }

        if (buildManager.hasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }

    }
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
