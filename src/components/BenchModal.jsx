import { useCallback, useEffect, useState } from "react";
import * as Dialog from "@radix-ui/react-dialog";
import Close from "../assets/close.svg";
import { useSearchParams } from "react-router-dom";
import axios from "axios";

import "./Edit.css";

function BenchModal({ getInfo }) {
  const [searchParams, setSearchParams] = useSearchParams();
  const id = searchParams.get("id");
  const [mensagem, setMensagem] = useState();
  
  useEffect(() => {
    
    async function fetchInfo(){
      if (id) {
        try {
          const info = await getInfo(id);
          setMensagem(info); 
        } catch (error) {
          console.error("Erro ao obter informações:", error);
          setMensagem(null); 
        }
      }
    };

    fetchInfo(); 

    return () => {
      setMensagem(null);
    };
  }, [getInfo]);

  const changeStatus = useCallback(() => {
    setSearchParams((state) => {
      if (id) {
        state.delete("id");
      }
      return state;
    });
  }, [id, setSearchParams]);

  return (
    <Dialog.Portal>
      <Dialog.Overlay className="DialogOverlay" />
      <Dialog.Content className="DialogContent">
        <Dialog.Close asChild>
          <button className="IconButton" aria-label="Close" onClick={changeStatus}>
            <img className="close" src={Close} alt="" />
          </button>
        </Dialog.Close>
        <Dialog.Title className="DialogTitle">Resultado do Benchmarking</Dialog.Title>
        <Dialog.Description className="DialogDescription">
         {mensagem&&(mensagem)}
        </Dialog.Description>
      </Dialog.Content>
    </Dialog.Portal>
  );
}

export default BenchModal;
