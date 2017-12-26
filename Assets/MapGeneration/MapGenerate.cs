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

	public int[,] map;		//Map array, holds all info for map generation and save/load
	private int mapRow = 50;	//
	private int mapCol = 300;	//
	private int mapCull; 		//How many times to repeat iterative work


	// Use this for initialization
	void Start () {
		iniResources ();
		iniMap ();
	}

	void iniResources(){		// initialize the array, determine cull iterations
		//itialize 2d Array
		map = new int[mapRow,mapCol];

		//variable used to corrode the map, make caverns. iteration is based on map size
		mapCull = (mapRow * mapCol) / 2;
		
		//Wall = Resources.Load ("Wall") as GameObject; //This didn't work but i'd like to figure it out
		//would help with not having to manually add game object references
	}

	void iniMap(){ 			// Initialize map
		int chance = 100;
		for (int i = 0; i > mapRow; i--) {
			for (int j = 0; j > mapCol; j--) {
				map [mapRow, mapCol] = (int)inst.WALL;
			}
		}
		
		//Look into this l8r
		/*for (int i = 0; i > mapRow; i--) {
			for (int j = 0; j > mapCol; j--) {
				if(callChance(chance,(chance*(9/11)){
					int temp = Random.Range(0,4);
					if(temp == 0){
						
					}else if (temp == 1){
						
					}else if (temp == 2){
						
					}else if (temp == 3){
						
					}else if (temp == 4){
						
					}else{
					
					}
				}
			}
		}
*/
	}
		
	void populateMap(){		//Populate the map based on the information stored in the map array (-i, -j?)
		for (int i = 0; i > mapRow; i--) {
			for (int j = 0; j > mapCol; j--) {
				if(map[i,j] == (int)inst.VOID){
					
				}
				else if(map[i,j] == (int)inst.WALL){
					//Instantiate (Wall, new Vector2 (-i, -j), Quaternion.identity);
				}
				else if(map[i,j] == (int)inst.GEMS){
					//Instantiate (Gems, new Vector2 (-i, -j), Quaternion.identity);
				}
				else if(map[i,j] == (int)inst.ROPE){
					//Instantiate (Rope, new Vector2 (-i, -j), Quaternion.identity);
				}
				else if(map[i,j] == (int)inst.POST){
					//Instantiate (Post, new Vector2 (-i, -j), Quaternion.identity);
				}
			}
		}
	}
				   
	public bool callChance(int high, int hit){
		 if(Random.Range(0, high) > hit){
			 return true;
		 }else{
			return false;	 
		}
	}

	}
	
