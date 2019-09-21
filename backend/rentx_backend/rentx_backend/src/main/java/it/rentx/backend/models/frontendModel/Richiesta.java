package it.rentx.backend.models.frontendModel;

public class Richiesta {
	
	private long id;
	
	private boolean b;
	
	private String s;

	public Richiesta() {
	}
	
	public Richiesta(long id, boolean b) {
		super();
		this.id = id;
		this.b = b;
	}


	public Richiesta(long id, boolean b, String s) {
		super();
		this.id = id;
		this.b = b;
		this.s = s;
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

	public String getS() {
		return s;
	}

	public void setS(String s) {
		this.s = s;
	}
	
	
	
	
}
