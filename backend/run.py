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


class Utente(db.Model):
  id = db.Column(db.Integer, primary_key=True, unique=True, nullable=False)
  username = db.Column(db.String(30), nullable=False, unique=True)
  email = db.Column(db.String(50), nullable=False, unique=True)
  password = db.Column(db.String(30), nullable=False)


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
  username = request.json['username']
  email = request.json['email']
  password = request.json['password']
  utente = Utente(username=username, email=email, password=password)
  db.session.add(utente)
  try:
      db.session.commit()
      return jsonify({'messaggio': 'Untente registrato correttamente'})
  except IntegrityError:
      db.session.rollback()
      return jsonify({'messaggio': 'Username o Email già presenti nel DB'})
      
'''
TESTING
'''

@auth.get_password
def get_pwd(username):
  utente = Utente.query.filter_by(username=username).first_or_404()
  if(utente is not None):
    return utente.password
  else:
    return jsonify({'messaggio': 'Utente non autorizzato perché non presente nel DB'})


@app.route('/prodotti', methods = ['GET'])
@auth.login_required
def prodotti():
  return jsonify({'message': 'Elenco prodotti'})

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





if __name__ == "__main__":
    app.run()
