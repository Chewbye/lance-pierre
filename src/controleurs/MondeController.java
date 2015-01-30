package controleurs;

import modeles.Monde;

public class MondeController {

	private Monde monde;
	
	private boolean moving = false;
	
	public MondeController(Monde monde){
		this.monde = monde;
	}
	
	/**
	 * Met à jour la position des éléments du monde en fonction du temps
	 * @param deltaTime temps
	 */
	public void update(float deltaTime) {
		
	}

}
