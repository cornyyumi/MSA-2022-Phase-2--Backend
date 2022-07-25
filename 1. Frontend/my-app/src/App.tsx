import { useState } from 'react';
import axios from "axios";
import './App.css';

function App() {
  const [cocktailName, setCocktailName] = useState("");
  
  const COCKTAIL_BASE_URL = "https://www.thecocktaildb.com/api/json/v1/1";
  
  return (
    <div>
      <h1>Title</h1>



      <div>
        <input onChange={e => setCocktailName(e.target.value)}></input>
        <button onClick={search}>
          Search
        </button>
      </div>


      <p>
        You have entered {cocktailName}
      </p>
    </div>
  );

  function search(){
    axios.get(COCKTAIL_BASE_URL + "/search.php?s=" + cocktailName).then((res) => {
      console.log(res.data);
    });
  }
}


export default App;