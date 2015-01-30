package modeles;

public class Cible extends Obstacle{
	private float rayon;
	private float points = 10;
	
	public Cible(float x, float y, float rayon){
		this.x = x;
		this.y = y;
		this.rayon = rayon;
	}
}
