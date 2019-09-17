package it.rentx.backend.models;

public class SearchQuery {
	
        private String oggetto;

        private String citta;

		public SearchQuery(String oggetto, String citta) {
			this.oggetto = oggetto;
			this.citta = citta;
		}

		public String getOggetto() {
			return oggetto;
		}

		public void setOggetto(String oggetto) {
			this.oggetto = oggetto;
		}

		public String getCitta() {
			return citta;
		}

		public void setCitta(String citta) {
			this.citta = citta;
		}
        
        
}
