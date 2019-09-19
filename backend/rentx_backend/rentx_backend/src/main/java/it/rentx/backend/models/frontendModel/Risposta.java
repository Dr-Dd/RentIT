package it.rentx.backend.models.frontendModel;

public class Risposta {

	private String hasSucceded;
	private Long id;
	private String accessToken;
	private String responseMessage;
	
	public Risposta(String hasSucceded, Long Id, String accessToken, String responseMessage) {
		this.hasSucceded = hasSucceded;
		this.id = Id;
		this.accessToken = accessToken;
		this.responseMessage = responseMessage;
	}
	
	public Risposta(String hasSucceded, String responseMessage,Long id) {
		this.hasSucceded = hasSucceded;
		this.responseMessage = responseMessage;
		this.id=id;
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

	public Long getId() {
		return id;
	}

	public void setUserId(Long id) {
		this.id = id;
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

	public void setId(Long id) {
		this.id = id;
	}
	
	
	
	
	
}
