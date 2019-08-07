package it.rentx.backend.models;

import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;
import javax.persistence.OneToOne;
import javax.persistence.Table;

@Entity
@Table(name = "utente")
public class Utente {
	
	@Id
	@GeneratedValue(strategy = GenerationType.SEQUENCE)
	private long id;
	
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
	
	@OneToMany(mappedBy = "utente", cascade = CascadeType.ALL)
	private List<Annuncio> annunci;
	
	@OneToMany
	private List<Annuncio> oggettiAffittati;

	@OneToOne(mappedBy = "utente", cascade = CascadeType.ALL, fetch = FetchType.LAZY, optional = false)
	private Image image;
	
	public Utente() {}
	
	public Utente(long id, String name, String surname, String email, String password,  String numero, String address, Image image) {
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

	public Image getImage() {
		return image;
	}

	public void setImage(Image image) {
		this.image = image;
	}
	
	
	
	
	
	
}
