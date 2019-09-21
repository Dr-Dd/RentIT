package it.rentx.backend.models.frontendModel;

public class Richiesta {
	
	private long id;
	
	private boolean b;

	public Richiesta() {
	}

	public Richiesta(long id, boolean b) {
		super();
		this.id = id;
		this.b = b;
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public boolean isB() {
		return b;
	}

	public void setB(boolean b) {
		this.b = b;
	}
	
	
	
	
}
