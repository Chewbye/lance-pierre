package controleurs;

import java.awt.Event;
import java.awt.Graphics;

import vues.MondeRenderer;
import modeles.LancePierre;
import modeles.Monde;

public class MoteurJeu {
	private Monde monde;
	private LancePierre lancePierre;
	private MondeRenderer renderer;
	private MondeController mondeController;
	
	public MoteurJeu(){
		
	}
	
	/** Gère l'évenement e récupéré du monde **/
	public boolean handleEvent(Event e) {
		switch (e.id) {
		case Event.KEY_PRESS:
		case Event.KEY_ACTION:
			// key pressed
			break;
		case Event.KEY_RELEASE:
			// key released
			break;
		case Event.MOUSE_DOWN:
			// mouse button pressed
			break;
		case Event.MOUSE_UP:
			// mouse button released
			break;
		case Event.MOUSE_MOVE:
			// mouse is being moved
			break;
		case Event.MOUSE_DRAG:
			// mouse is being dragged (button pressed)
			break;
		}
		return false;
	}
	
	/** la méthode de mise à jour avec le deltaTime en secondes **/
	public void update(float deltaTime) {
		mondeController.update(deltaTime);
	}
	
	/** Affichage du monde et de ses composants**/
	public void render(Graphics g) {
		renderer.render(g);
	}
}
