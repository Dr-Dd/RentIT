package it.rentx.backend.models;

public class Risposta {

	private String message;
	private String status;
	
	public Risposta(String msg, String status) {
		this.message = msg;
		this.status = status;
	}

	public String getMessage() {
		return message;
	}

	public void setMessage(String message) {
		this.message = message;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}
	
	
}
