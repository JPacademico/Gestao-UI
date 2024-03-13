import { Link } from "react-router-dom";
import * as Dialog from "@radix-ui/react-dialog";
import Close from "../assets/close.svg";

import "./Edit.css";
import { useState } from "react";

function BenchModal({ createNewModal }) {
  const [nameProduto, setName] = useState();
  const [estoque, setEstoque] = useState();
  const [estoqueMin, setMinStorage] = useState();

  const handleSubmit = (e) => {
    e.preventDefault();

    createNewModal({ name, price:0, estoque, estoqueMin});

    setName("");
    setEstoque("");
    setMinStorage("");
  };

  return (
    <Dialog.Portal>
      <Dialog.Overlay className="DialogOverlay" />
      <Dialog.Content className="DialogContent">
        <Dialog.Close asChild>
          <button className="IconButton" aria-label="Close">
            <img className="close" src={Close} alt="" />
          </button>
        </Dialog.Close>
        <Dialog.Title className="DialogTitle">Adicionar Produto</Dialog.Title>
        <Dialog.Description className="DialogDescription">
          Preencha os campos com o novo produto e clique em "Adicionar"
        </Dialog.Description>
        
      </Dialog.Content>
    </Dialog.Portal>
  );
}

export default BenchModal;
