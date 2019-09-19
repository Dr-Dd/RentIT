package it.rentx.backend.models.frontendModel;


public class UtenteModel {
	
	private boolean isFirstAccess;
	
	private String name;
	
	private String surname;
	
	private String email;
	
	private String password;
	
	private String numeroTel;
	
	private String citta;
	
	private byte[] fotoProfilo;
	
	
	
	
	
	
	public UtenteModel(String name, String surname, String email, String password,  String numeroTel, String citta, byte[] fotoProfilo) {
		super();
		this.name = name;
		this.surname = surname;
		this.email = email;
		this.password = password;
		this.numeroTel = numeroTel;
		this.citta = citta;
		this.fotoProfilo = fotoProfilo;
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public boolean isFirstAccess() {
		return isFirstAccess;
	}

	public void setFirstAccess(boolean isFirstAccess) {
		this.isFirstAccess = isFirstAccess;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getSurname() {
		return surname;
	}

	public void setSurname(String surname) {
		this.surname = surname;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public String getNumeroTel() {
		return numeroTel;
	}

	public void setNumeroTel(String numeroTel) {
		this.numeroTel = numeroTel;
	}

	public String getCitta() {
		return citta;
	}

	public void setCitta(String citta) {
		this.citta = citta;
	}

	public byte[] getFotoProfilo() {
		return fotoProfilo;
	}

	public void setFotoProfilo(byte[] fotoProfilo) {
		this.fotoProfilo = fotoProfilo;
	}

}
