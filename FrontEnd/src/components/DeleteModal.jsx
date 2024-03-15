import { useSearchParams } from "react-router-dom";
import * as Dialog from "@radix-ui/react-dialog";
import Close from '../assets/close.svg'
import axios from "axios";
import "./Edit.css";
import { useCallback, useState } from "react";
import api from "../lib/api";
import { useEffect } from "react";

function Delete({toggleModalStatus, getProducts}) {
    const [list, setList] = useState();
  const [searchParams, setSearchParams] = useSearchParams() 
  
  const id = searchParams.get('id')
  
  

const handleDeleteProduct = useCallback(async () => {
    await axios.delete(`https://localhost:8020/api/Produtos/${id}`);
    console.log(id);
    getProducts();
  }, [id]);

//   useEffect(() => 
//   {
//     getData()
//   }, []);

  return (
    <Dialog.Portal>
      <Dialog.Overlay className="DialogOverlay" />
      <Dialog.Content className="DialogContent">
        <Dialog.Close asChild>
          <button className="IconButton" aria-label="Close" onClick={()=>{
            if (id) {
            const newSearchParams = new URLSearchParams(searchParams);
            newSearchParams.delete('id');
            setSearchParams(newSearchParams);
            }}}>
            <img className="close" src={Close} alt="" />
          </button>
        </Dialog.Close>
        <Dialog.Title className="DialogTitle">Deletar Produto</Dialog.Title>
        <Dialog.Description className="DialogDescription">
          Esse produto será excluído permanentemente. Você tem certeza?
        </Dialog.Description>
        


          <div
            style={{
              display: "flex",
              marginTop: 25,
              justifyContent: "flex-end",
            }}
          >
            <button type="submit" className="Button red"
             onClick={() => {
                handleDeleteProduct();
                toggleModalStatus();
                }}>
              Deletar
            </button>
          </div>

      </Dialog.Content>
    </Dialog.Portal>
  );
}

export default Delete;
