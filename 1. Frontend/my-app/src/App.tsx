import { useState } from 'react';
import { Button, InputGroup} from "@blueprintjs/core";
import { Book } from './model/book';
import { Author } from './model/author';

import axios from "axios";
import './App.css';
import 'normalize.css/normalize.css';
import '@blueprintjs/core/lib/css/blueprint.css';
import { resourceLimits } from 'worker_threads';


function App() {
  <link href="path/to/node_modules/normalize.css/normalize.css" rel="stylesheet" />

  const enterButton = (
    <Button icon='key-enter' onClick={search}>Search</Button>
  );

  const [bookID, setBookId] = useState("");
  const [bookInfo, setBookInfo] = useState<undefined | null | Book>(undefined); 
  const authorInfo = new Array<Author>;


  const BOOK_BASE_URL = "https://openlibrary.org/isbn/";
  
  return (

    <div style={{textAlign:'center', marginTop:'10%', alignItems:'center', width:'100%', backgroundColor: "white"}}>
      <h1>Search for a book by their ISBN</h1>

      <div style={{alignItems:'center', width:800, margin: '50px auto',}}>
        <InputGroup large={true} placeholder='Enter ISBN' leftIcon='search' rightElement={enterButton} onChange={e => setBookId(e.target.value)}></InputGroup>
      </div>


      {bookInfo === undefined || bookInfo === null ? (
        <p>Book not found</p>
      ) : (
        <div className='bookMain' style={{display: 'flex', justifyContent: 'center', margin: '0px 0px 100px 100px'}}>
          <div className='bookLeft' style={{padding:25}}>
            <img src={"https://covers.openlibrary.org/b/id/" + bookInfo.covers[0] + "-L.jpg"}></img>
          </div>
          <div className='bookRight' style={{padding:25, width:'50%', textAlign:'left'}}>
            <h2>{bookInfo.title}</h2>
            <h3>Summary</h3>
            <p>{bookInfo.description}</p>
          </div>
        </div>
      )}
    </div>
    
  );



  function search(){
    axios.get(BOOK_BASE_URL + bookID + ".json").then((res) => {
      console.log(res.data);
      if (res.data.hasOwnProperty('description')===false){
        searchWorks(res.data.works[0].key);
        
      }else{
        setBookInfo(res.data);
      }
      if (bookInfo!=null || bookInfo!=undefined){
        bookInfo.authors.forEach(function(author : any){
          console.log(author["author"].key);
          searchAuthors(author["author"].key);
        });
      }
      console.log(authorInfo);

    }).catch(() => {
      setBookInfo(null);
    });
  }

  function searchWorks(worksId: string){    
    axios.get("https://openlibrary.org" + (worksId) + ".json").then((res) => {
      setBookInfo(res.data);
      console.log(res.data);
    }).catch(() => {
      setBookInfo(null);
    });
  }

  function searchAuthors(authorId: string){
    axios.get("https://openlibrary.org" + authorId + ".json").then((res) => {
      console.log(res.data["name"]);
    })
  }
}


export default App;