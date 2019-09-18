package it.rentx.backend.models;

import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
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
	
	@Column(name = "first_access")
	private boolean isFirstAccess = true;
	
	@Column(name = "name_utente", nullable = false)
	private String name;
	
	@Column(name = "surname_utente", nullable = false)
	private String surname;
	
	@Column(name = "email_utente", nullable = false, unique = true)
	private String email;
	
	@Column(name = "password_utente", nullable = false)
	private String password;
	
	@Column(name = "numero_utente")
	private String numero;
	
	@Column(name = "address_utente")
	private String address;
	
	@Lob
	@Column(name = "immagine_utente")
	private byte[] image;
	
	@OneToMany(mappedBy = "affittuario", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
	private List<Annuncio> annunciUtente;
	
	public Utente() {}
	
	public Utente(long id, String name, String surname, String email, String password,  String numero, String address, byte[] image) {
		this.name = name;
		this.surname = surname;
		this.email = email;
		this.password = password;
		this.numero = numero;
		this.address = address;
		this.image = image;
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

	public String getNumero() {
		return numero;
	}

	public void setNumero(String numero) {
		this.numero = numero;
	}

	public String getAddress() {
		return address;
	}

	public void setAddress(String address) {
		this.address = address;
	}

	public byte[] getImage() {
		return image;
	}

	public void setImage(byte[] image) {
		this.image = image;
	}

	public List<Annuncio> getAnnunciUtente() {
		return annunciUtente;
	}

	public void setAnnunciUtente(List<Annuncio> annunciUtente) {
		this.annunciUtente = annunciUtente;
	}
	
	
	
	
	
	
	
	
	
}
