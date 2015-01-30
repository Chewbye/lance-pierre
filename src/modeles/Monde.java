package modeles;

import java.util.List;

public class Monde {
	private int width;
	private int height;
	private Object[][] grille;
	private List<Obstacle> cibles;
	private LancePierre lancePierre;
	
	public Monde(int width, int height, Object[][] grille, List<Obstacle> cibles, LancePierre lancePierre){
		this.width = width;
		this.height = height;
		this.cibles = cibles;
		this.lancePierre = lancePierre;
	}
}
