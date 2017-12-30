using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour {

	public GameObject Wall;
	public GameObject Gems;
	public GameObject Rope;
	public GameObject Post;
	enum inst {VOID, WALL, GEMS, ROPE, POST};

	//public GameObject player;

	public int[,,] map;		//Map array, holds all info for map generation and save/load

	private int mapRow = 50;	//
	private int mapCol = 300;	//
	private int mapBroke = 2;
	private int mapDepth = 103;


	// Use this for initialization
	void Start () {
		iniResources ();
		iniMap ();
		populateMap ();
	}

	void iniResources(){		// initialize the array, determine cull iterations
		//itialize 2d Array
		map = new int[mapRow,mapCol,mapDepth];
		mapDepth = mapDepth - 1;
				
		//Wall = Resources.Load ("Wall") as GameObject; //This didn't work but i'd like to figure it out
		//would help with not having to manually add game object references
	}

	void iniMap(){ 			// Initialize map
		int chance = 100;

		//Sets all blocks to initially be WALL
		for (int i = 1; i < mapRow-1; i++) {
			for (int j = 1; j < mapCol-1; j++) {
				//Debug.Log ("i" + i + " j" + j );
				map [i, j, 0] = (int)inst.WALL;
			}
		}
		
		//*/Random chance to change block FROM Wall, TO another inst
		for (int i = 0; i < mapRow; i++) {
			for (int j = 0; j < mapCol; j++) {
				if(callChance(chance,((chance)*9/11))){
					int temp = Random.Range(0,5);
					Debug.Log (temp);
					if(temp == 0){
						map [i, j, 0] = (int)inst.VOID;
					}else if (temp == 1){
						map [i, j, 0] = (int)inst.WALL;
						//changes nothing
					}else if (temp == 2){
						map [i, j, 0] = (int)inst.GEMS;
					}else if (temp == 3){
						map [i, j, 0] = (int)inst.ROPE;
					}else if (temp == 4){
						map [i, j, 0] = (int)inst.POST;
					}else{
					
					}
				}
			}
		}
//*/
	}
		
	void populateMap(){		//Populate the map based on the information stored in the map array (-i, -j?)
		for (int i = 1; i < mapRow; i++) {
			for (int j = 1; j < mapCol; j++) {
				//Debug.Log ("i" + i + " j" + j + "Populate");
				if(map[i,j,0] == (int)inst.VOID){
				}
				else if(map[i,j,0] == (int)inst.WALL){
					With (Instantiate (Wall, new Vector3 ((mapRow/2)-i, -j, mapDepth), Quaternion.identity),i,j);		
				}
				else if(map[i,j,0] == (int)inst.GEMS){
					With(Instantiate (Gems, new Vector3 ((mapRow/2)-i, -j, mapDepth), Quaternion.identity),i,j);
				}
				else if(map[i,j,0] == (int)inst.ROPE){
					With(Instantiate (Rope, new Vector3 ((mapRow/2)-i, -j, mapDepth), Quaternion.identity),i,j);
				}
				else if(map[i,j,0] == (int)inst.POST){
					With(Instantiate (Post, new Vector3 ((mapRow/2)-i, -j, mapDepth), Quaternion.identity),i,j);
				}
			}
		}
	}

	public void With(GameObject inst, int x, int y){
		map[x,y,mapDepth] = inst.GetInstanceID();
		//Debug.Log (map [x, y, mapDepth]);
		inst.name = inst.GetInstanceID().ToString();
	}
				   
	public bool callChance(int high, int hit){
		 if(Random.Range(0, high) > hit){
			 return true;
		 }else{
			return false;	 
		}
	}

	}
	
