from flask import Flask, request, jsonify
from flask_sqlalchemy import SQLAlchemy
from flask_httpauth import HTTPBasicAuth
from sqlalchemy.exc import IntegrityError
from werkzeug.security import generate_password_hash, check_password_hash
import json

# Creo l'app Flask
app = Flask(__name__) #app è un oggetto di tipo Flask

# Auth
auth = HTTPBasicAuth()

# Configurazione DB (sqlite)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///tmp/test.db'
db = SQLAlchemy(app) #creo db con oggetto all'interno di tipo Flask

#Creazione in alchemy di una tabella
class Utente(db.Model):
  id = db.Column(db.Integer, primary_key=True, unique=True, nullable=False)
  name = db.Column(db.String(30), nullable=False)
  surname = db.Column(db.String(30), nullable=False)
  email = db.Column(db.String(50), nullable=False, unique=True)
  password = db.Column(db.String(30), nullable=False)

def __init__(self,name,surname,email,password):
  self.name=name
  self.surname=surname
  self.email=email
  self.password=password

class Prodotto(db.Model):
  id = db.Column(db.Integer, primary_key = True, unique = True, nullable = False)
  code = db.Column(db.Integer, unique = True, nullable = False)
  name = db.Column(db.String(50), nullable = False)
  category = db.Column(db.String(30), nullable = False)
  utenteA = db.Column(db.String(50), nullable = False)
  utenteB = db.Column(db.String(50), nullable = True)
  price = db.Column(db.Integer, nullable = False)

  def __init__ (self, code, name, category, utenteA, UtenteB, price):
    self.code = code
    self.name = name
    self.category = category
    self.utenteA = utenteA
    self.utenteB = utenteB
    self.price = price

# Creazione tabelle
db.create_all()


'''
INIZIO ENDPOINTS API
'''

# PAGINA DI ROOT
@app.route('/')
def index():
  return jsonify({'messaggio': 'Fratè ti devi registrare. DAJE!'})

# REGISTRAZIONE UTENTE
@app.route('/registrazione', methods=['POST'])
def registrazione():
  name = request.json['name']
  surname = request.json['surname']
  email = request.json['email']
  password = request.json['password']
  utente = Utente(name=name, surname=surname, email=email, password=password)
  db.session.add(utente)
  try:
      db.session.commit()
      return 'Untente registrato correttamente'
  except IntegrityError:
      db.session.rollback()
      return 'Username o Email già presenti nel DB'
      
'''
TESTING
'''

@auth.get_password
def get_pwd(username):
  utente = Utente.query.filter_by(username=username).first_or_404()
  if(utente is not None):
    return utente.password
  else:
    return 'Utente non autorizzato perché non presente nel DB'

@app.route('/login', methods = ['POST'])
def login():
  email = request.json['email']
  password = request.json['password']
  if(password == get_pwd(email)):
    return 'Login effettuato'
  else:
    return 'email o password errati'

#DA IMPLEMENTARE
@app.route('/prodotti', methods = ['GET'])
@auth.login_required
def prodotti():
  return 'Elenco prodotti'


@app.route('/delete', methods = ['DELETE'])

def cancella():
  email = request.json['email']
  utente = Utente.query.filter_by(email=email).first()
  if(utente is not None):
    db.session.delete(utente)
    try:
      db.session.commit()
      return 'Utente eliminato'
    except:
      db.session.rollback()
      return 'L\'utente non è presente nel DB'
  else:
    return 'L\'utente non è presente nel DB'




if __name__ == "__main__":
    app.run(debug=True,port='5000')
