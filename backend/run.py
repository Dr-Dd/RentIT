from flask import Flask, request, jsonify
from flask_sqlalchemy import SQLAlchemy
from flask_httpauth import HTTPBasicAuth
from sqlalchemy.exc import IntegrityError
from werkzeug.security import generate_password_hash, check_password_hash
import json
# Creo l'app Flask
app = Flask(__name__)

# Auth
auth = HTTPBasicAuth()

# Configurazione DB (sqlite)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///tmp/test.db'
db = SQLAlchemy(app)

# Creazione Tabella
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
def get_pwd(email):
  utente = Utente.query.filter_by(email=email).first()
  if(utente is not None):
    return utente.password
  else:
    return 'Utente non iscritto. Iscriviti per poter effettuare il login'


@app.route('/login', methods = ['POST'])
def login():
  email = request.json['email']
  password = request.json['password']
  if(password == get_pwd(email)):
    return 'Login effettuato'
  else:
    return 'email o password errati'
  

'''
@app.route('/delete', methods = ['DELETE'])
@auth.login_required
def cancella():
  username = request.json['username']
  utente = Utente.query.filter_by(username=username).first()
  if(utente is not None):
    db.session.delete(utente)
    try:
      db.session.commit()
      return jsonify({'messaggio': 'Utente eliminato'})
    except:
      db.session.rollback()
      return jsonify({'messaggio': 'L\'utente non è presente nel DB'})
  else:
    return jsonify({'messaggio': 'L\'utente non è presente nel DB'}), 404
'''



if __name__ == "__main__":
    app.run(debug=True,port='5000',host='0.0.0.0')
