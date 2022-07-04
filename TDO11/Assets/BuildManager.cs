using UnityEngine;

public class BuildManager : MonoBehaviour
{
    #region Singleton
    //signalton
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("il y a deja une BuildManager dans la scene !");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject buildEffect;
    public GameObject sellEffect;

    //contient seulement la trourelle qu'on veut apparetre
    private TurretBlueprint turretToBuild;

    //la node qui est selectionnee
    private Node selectedNode;

    public NodeUI nodeUI;

    //public GameObject GetTurretToBuild()
    //{
    //    return turretToBuild;
    //}
    public bool canBuild { get { return turretToBuild != null; } }
    public bool hasMoney { get { return PlayerStats.money >= turretToBuild.cost; } }

    //ça nous permettre quelle tourelle on va construire
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    //notree getteur qui nous permettre de recuper la valeur de turretToBuild sans la modifiere
    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

    //ça nous permettre quelle node on va construire
    public void SelectNode(Node node)
    {
        if (node == selectedNode)
        {
            DeselectNode();
            return;
        }


        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

}
