package it.rentx.backend.models;

import java.util.List;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Lob;
import javax.persistence.OneToMany;
import javax.persistence.Table;

@Entity
@Table(name = "utente")
public class Utente {
	
	@Id
	@GeneratedValue(strategy = GenerationType.SEQUENCE)
	private long id;
	
	private boolean isFirstAccess = true;
	
	@Column(nullable = false)
	private String name;
	
	@Column(nullable = false)
	private String surname;
	
	@Column(nullable = false, unique = true)
	private String email;
	
	@Column(nullable = false)
	private String password;
	
	private String numeroTel;
	
	private String citta;
	
	@Lob
	@Column(name = "foto_profilo", length = 100000)
	private byte[] fotoProfilo;
	
	@OneToMany(mappedBy = "affittuario")
	private List<Annuncio> annunciUtente;
	
	public Utente() {}
	
	public Utente(String name, String surname, String email, String password,  String numeroTel, String citta, byte[] fotoProfilo) {
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

	public List<Annuncio> getAnnunciUtente() {
		return annunciUtente;
	}

	public void setAnnunciUtente(List<Annuncio> annunciUtente) {
		this.annunciUtente = annunciUtente;
	}

	
}
