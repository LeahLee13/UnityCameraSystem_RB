using UnityEngine;
using System.Collections.Generic;

public class Obstacle2Transparente : MonoBehaviour
{
    private GameObject player;
    public Material alphaMaterial;//͸������

    private List<RaycastHit> hits;
    private List<HitInfo> changedInfos = new List<HitInfo>();

    private struct HitInfo
    {
        public GameObject obj;
        public Renderer[] renderers;
        public List<Material> materials;
    }

    void Update()
    {
        if (player == null)
        {
            player = GameManager.Instance.playerObj;
            if (player == null) { return; }
        }

        ChangeMaterial();
    }

    private void ChangeMaterial()
    {
        //�����λ�����ɫλ�õĲ�ֵ-1��Ϊ�˱����⵽���棬��߿���ֲ�ԣ��������߼�⵽�Ķ���Ҫ��ȥ����
        hits = new List<RaycastHit>(Physics.RaycastAll(transform.position, transform.forward, Vector3.Distance(this.transform.position, player.transform.position) - 1));

        //�滻����
        for (int i = 0; i < hits.Count; i++)
        {
            //���߼�⵽�Ķ����ȥ��ɫ
            if (hits[i].collider.gameObject.name != player.name)
            {
                var hit = hits[i];
                int findIndex = changedInfos.FindIndex(item => item.obj == hit.collider.gameObject);
                var rendArray = hit.collider.gameObject.GetComponentsInChildren<Renderer>();

                if (rendArray.Length > 0)
                {
                    //û�ҵ������
                    if (findIndex < 0)
                    {
                        var changed = new HitInfo();
                        changed.obj = hit.collider.gameObject;
                        changed.renderers = rendArray;
                        changed.materials = new List<Material>();

                        for (int j = 0; j < rendArray.Length; j++)
                        {
                            var materials = rendArray[j].materials;
                            var tempMaterials = new Material[materials.Length];

                            for (int k = 0; k < materials.Length; k++)
                            {
                                changed.materials.Add(materials[k]);
                                tempMaterials[k] = alphaMaterial;
                            }

                            rendArray[j].materials = tempMaterials;//�滻����
                        }

                        changedInfos.Add(changed);
                    }
                }
            }
        }

        //��ԭ����
        for (int i = 0; i < changedInfos.Count;)
        {
            var changedInfo = changedInfos[i];
            var findIndex = hits.FindIndex(item => item.collider.gameObject == changedInfo.obj);

            //û�ҵ����Ƴ�
            if (findIndex < 0)
            {
                if (changedInfo.obj != null)
                {
                    foreach (var renderer in changedInfo.renderers)
                    {
                        renderer.materials = changedInfo.materials.ToArray();//��ԭ����
                    }
                }

                changedInfos.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }
}
