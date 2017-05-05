using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomUnitGenerator : MonoBehaviour, IUnitGenerator
{
    public Transform UnitsParent;
    public Transform CellsParent;
	public GameObject enemy;
	public Unit player1;
	public Unit player2;
	public Unit player3;
	public Unit player4;
	public float spawnAreaCoef = 0.8f;
	public int minimumEnemies = 2;
	public float difficulty = 4.0f;

    public List<Unit> SpawnUnits(List<Cell> cells)
    {
		List<Unit> companions = new List<Unit> ();
		companions.Add(player1.GetComponent<Unit>());
		companions.Add(player2.GetComponent<Unit>());
		companions.Add(player3.GetComponent<Unit>());
		companions.Add(player4.GetComponent<Unit>());
		List<Unit> adversaries = new List<Unit> ();
		int enemies=Mathf.RoundToInt(UnityEngine.Random.Range(minimumEnemies,minimumEnemies+difficulty));

		for (int i=0; i<enemies; i++){
			GameObject e = Instantiate (enemy);
			adversaries.Add (e.GetComponent<Unit>());
			adversaries [i].GetComponent<Unit> ().name = "Enemy" + i;
		}

        List<Unit> ret = new List<Unit>();
		float cellMin = cells [0].transform.position.x;
		float cellMax = cells [0].transform.position.x;
		float cellDist = Mathf.Abs(cells [0].transform.position.y - cells [1].transform.position.y);

		for (int i = 1; i < cells.Count; i++) {
			if (cellMin > cells [i].transform.position.x) cellMin = cells [i].transform.position.x;
			else if (cellMax < cells [i].transform.position.x) cellMax = cells [i].transform.position.x;
		}

		float cellMid = cellMax / 2.0f;
		List<Cell> leftSide = new List<Cell> ();
		List<Cell> rightSide = new List<Cell> ();

		for (int i = 0; i < cells.Count; i++) {
			if (cellMid * spawnAreaCoef > cells [i].transform.position.x) {
				leftSide.Add (cells [i]);
				cells [i].GetComponent<SpriteRenderer> ().color = new Color (0.7f, 0.7f, 1.0f);
			}
			else if (cellMax-cellMid * spawnAreaCoef < cells [i].transform.position.x) {
				rightSide.Add (cells [i]);
				cells [i].GetComponent<SpriteRenderer> ().color = new Color (1.0f, 0.7f, 0.7f);
			}
		}

		Cell groupDrop = leftSide [Mathf.RoundToInt (UnityEngine.Random.Range (0.0f, leftSide.Count-1))];
		groupDrop.GetComponent<SpriteRenderer> ().color = new Color (0.0f, 0.0f, 1.0f);

		List<Cell> groupArea = new List<Cell> ();

		if (companions.Count > 1) {
			for (int i = 0; i < leftSide.Count; i++) {
				if ((leftSide [i].transform.position.x>groupDrop.transform.position.x-cellDist*companions.Count)&&
					(leftSide [i].transform.position.x<groupDrop.transform.position.x+cellDist*companions.Count)&&
					(leftSide [i].transform.position.y>groupDrop.transform.position.y-cellDist*companions.Count)&&
					(leftSide [i].transform.position.y<groupDrop.transform.position.y+cellDist*companions.Count)
				) {
					groupArea.Add (leftSide [i]);
					leftSide [i].GetComponent<SpriteRenderer> ().color = new Color (0.3f, 0.3f, 1.0f);
				}
			}
			for (int i = 0; i < companions.Count; i++) {
				int placeTo=Mathf.RoundToInt(UnityEngine.Random.Range(0.0f,groupArea.Count-1.0f));
				groupArea[placeTo].IsTaken = true;
				companions[i].Cell = groupArea[placeTo];
				companions[i].transform.position = groupArea[placeTo].transform.position;
				companions[i].Initialize();
				ret.Add(companions[i]);
				groupArea.RemoveAt (placeTo);
			}
		} else {
			groupDrop.IsTaken = true;
			companions[0].Cell = groupDrop;
			companions[0].transform.position = groupDrop.transform.position;
			companions[0].Initialize();
			ret.Add(companions[0]);
		}

		groupArea.Clear();

		groupDrop = rightSide [Mathf.RoundToInt (UnityEngine.Random.Range (0.0f, rightSide.Count-1))];

		if (adversaries.Count > 1) {
			for (int i = 0; i < rightSide.Count; i++) {
				if ((rightSide [i].transform.position.x>groupDrop.transform.position.x-cellDist*adversaries.Count)&&
					(rightSide [i].transform.position.x<groupDrop.transform.position.x+cellDist*adversaries.Count)&&
					(rightSide [i].transform.position.y>groupDrop.transform.position.y-cellDist*adversaries.Count)&&
					(rightSide [i].transform.position.y<groupDrop.transform.position.y+cellDist*adversaries.Count)
				) {
					groupArea.Add (rightSide [i]);
					rightSide [i].GetComponent<SpriteRenderer> ().color = new Color (1.0f, 0.3f, 0.3f);
				}
			}
			for (int i = 0; i < adversaries.Count; i++) {
				int placeTo=Mathf.RoundToInt(UnityEngine.Random.Range(0.0f,groupArea.Count-1.0f));
				groupArea[placeTo].IsTaken = true;
				adversaries[i].Cell = groupArea[placeTo];
				adversaries[i].transform.position = groupArea[placeTo].transform.position;
				adversaries[i].Initialize();
				ret.Add(adversaries[i]);
				groupArea.RemoveAt (placeTo);
			}
		} else {
			groupDrop.IsTaken = true;
			adversaries[0].Cell = groupDrop;
			adversaries[0].transform.position = groupDrop.transform.position;
			adversaries[0].Initialize();
			ret.Add(adversaries[0]);
		}

		return ret;
		/*
        for (int i = 0; i < UnitsParent.childCount; i++)
        {
            var unit = UnitsParent.GetChild(i).GetComponent<Unit>();
            if(unit !=null)
            {
                var cell = cells.OrderBy(h => Math.Abs((h.transform.position - unit.transform.position).magnitude)).First();
                if (!cell.IsTaken)
                {
                    cell.IsTaken = true;
                    unit.Cell = cell;
                    unit.transform.position = cell.transform.position;
                    unit.Initialize();
                    ret.Add(unit);
                }//Unit gets snapped to the nearest cell
                else
                {
                    Destroy(unit.gameObject);
                }//If the nearest cell is taken, the unit gets destroyed.
            }
        }
        return ret;
*/
    }

    public void SnapToGrid()
    {
        List<Transform> cells = new List<Transform>();

        foreach(Transform cell in CellsParent)
        {
            cells.Add(cell);
        }

        foreach(Transform unit in UnitsParent)
        {
            var closestCell = cells.OrderBy(h => Math.Abs((h.transform.position - unit.transform.position).magnitude)).First();
            if (!closestCell.GetComponent<Cell>().IsTaken)
            {
                Vector3 offset = new Vector3(0,0, closestCell.GetComponent<Cell>().GetCellDimensions().z);
                unit.position = closestCell.transform.position - offset;
            }//Unit gets snapped to the nearest cell
        }
    }
}

