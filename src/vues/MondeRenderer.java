package vues;

import java.awt.Graphics;

import modeles.Monde;

public class MondeRenderer implements Renderer{

	private Monde monde;
	
	/**
	 * Construit la vue du monde du jeu
	 * @param monde Modèle de représentation du monde
	 */
	public MondeRenderer(Monde monde){
		this.monde = monde;
	}
	
	/**
	 * Dessine le monde sur l'objet g
	 * @param g Panneau où va être dessiné le monde
	 */
	@Override
	public void render(Graphics g) {
		
	}

}
