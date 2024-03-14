import { Link } from "react-router-dom";
import * as Dialog from "@radix-ui/react-dialog";
import Close from "../assets/close.svg";
import { useSearchParams } from "react-router-dom";
import { useCallback } from "react";

import "./Edit.css";
import { useState } from "react";
import axios from "axios";

function BenchModal({getInfo}) {
  const [searchParams, setSearchParams] = useSearchParams();

  const id = searchParams.get("id");
  
  const changeStatus = useCallback(async (id) => {

    await getInfo();

    setSearchParams((state) => {
      if (id) {
        state.delete("id");
      }
      return state;
    });
  });
  
  getInfo(id)

  return (
    <Dialog.Portal>
      <Dialog.Overlay className="DialogOverlay" />
      <Dialog.Content className="DialogContent">
        <Dialog.Close asChild>
          <button className="IconButton" aria-label="Close" onClick={()=>changeStatus}>
            <img className="close" src={Close} alt="" />
          </button>
        </Dialog.Close>
        <Dialog.Title className="DialogTitle">Resultado do Benchmarking</Dialog.Title>
        <Dialog.Description className="DialogDescription">
        
        </Dialog.Description>
        
        
      </Dialog.Content>
    </Dialog.Portal>
  );
}

export default BenchModal;
