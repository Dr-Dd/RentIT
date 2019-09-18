package it.rentx.backend.models;

public class Risposta {

	private String hasSucceded;
	private Long userId;
	private String accessToken;
	private String responseMessage;
	private Utente utente;
	
	public Risposta(String hasSucceded, Long userId, String accessToken, String responseMessage) {
		this.hasSucceded = hasSucceded;
		this.userId = userId;
		this.accessToken = accessToken;
		this.responseMessage = responseMessage;
	}
	
	public Risposta(String hasSucceded, String accessToken, String responseMessage, Utente utente) {
		this.hasSucceded = hasSucceded;
		this.accessToken = accessToken;
		this.responseMessage = responseMessage;
		this.utente = utente;
	}
	
	public Risposta(String hasSucceded, String responseMessage) {
		this.hasSucceded = hasSucceded;
		this.responseMessage = responseMessage;
	}

	public String getHasSucceded() {
		return hasSucceded;
	}

	public void setHasSucceded(String hasSucceded) {
		this.hasSucceded = hasSucceded;
	}

	public Long getUserId() {
		return userId;
	}

	public void setUserId(Long userId) {
		this.userId = userId;
	}

	public String getAccessToken() {
		return accessToken;
	}

	public void setAccessToken(String accessToken) {
		this.accessToken = accessToken;
	}

	public String getResponseMessage() {
		return responseMessage;
	}

	public void setResponseMessage(String responseMessage) {
		this.responseMessage = responseMessage;
	}

	public Utente getUtente() {
		return utente;
	}

	public void setUtente(Utente utente) {
		this.utente = utente;
	}
	
	
	
	
}
