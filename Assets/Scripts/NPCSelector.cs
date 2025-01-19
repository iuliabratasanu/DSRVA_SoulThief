using System.Collections.Generic;
using UnityEngine;


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSelector : MonoBehaviour
{
    public Transform player; // jucatorul
    public List<Transform> npcs; // lista cu npc-uri
    public float selectionRadius = 5f; // raza de selectie
    public GameObject selectionBorderPrefab; // prefab pentru bordura
    public Canvas selectionCanvas; // canvas pentru UI

    private Transform closestNPC = null;
    private GameObject currentBorder = null;

    void Update()
    {
        HighlightClosestNPC(); // actualizeaza npc-ul cel mai apropiat
    }

    void HighlightClosestNPC()
    {
        float closestDistance = selectionRadius;
        Transform selectedNPC = null;

        // cauta npc-ul cel mai apropiat in raza de selectie
        foreach (Transform npc in npcs)
        {
            float distance = Vector3.Distance(player.position, npc.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                selectedNPC = npc;
            }
        }

        // daca npc-ul cel mai apropiat este diferit de cel selectat anterior
        if (selectedNPC != closestNPC)
        {
            // distruge bordura anterioara daca exista
            if (currentBorder != null)
            {
                Destroy(currentBorder);
            }

            // daca un nou npc este selectat in raza
            if (selectedNPC != null)
            {
                // instantierea bordurii si setarea la copil al canvas-ului
                currentBorder = Instantiate(selectionBorderPrefab, selectionCanvas.transform);

                // converteste pozitia npc-ului din lumea 3D in pozitie pe ecran
                Vector3 screenPos = Camera.main.WorldToScreenPoint(selectedNPC.position);
                currentBorder.GetComponent<RectTransform>().position = screenPos;

                // ajusteaza dimensiunea si pozitia bordurii
                RectTransform rectTransform = currentBorder.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(100, 100); // dimensiune exemplu, ajusteaza daca e necesar
                rectTransform.anchoredPosition = Vector2.zero; // asigura centrare pe npc
            }

            // actualizeaza referinta la npc-ul cel mai apropiat
            closestNPC = selectedNPC;
        }
        else if (closestNPC != null)
        {
            // daca npc-ul cel mai apropiat este inca in raza de selectie, actualizeaza pozitia bordurii
            Vector3 screenPos = Camera.main.WorldToScreenPoint(closestNPC.position);
            currentBorder.GetComponent<RectTransform>().position = screenPos;
        }
    }
}