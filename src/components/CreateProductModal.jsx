import { Link } from "react-router-dom";
import * as Dialog from "@radix-ui/react-dialog";
import Close from "../assets/close.svg";

import "./Edit.css";
import { useState } from "react";

function AddModal({ createNewModal }) {
  const [name, setName] = useState();
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
        <form onSubmit={handleSubmit}>
          <label className="Label" htmlFor="name">
            Descrição
          </label>
          <input
            type="text"
            required
            className="Input"
            id="name"
            defaultValue=""
            onChange={(e) => setName(e.target.value)}
            value={name}
          />

          <label className="Label" htmlFor="username">
            Estoque Atual
          </label>
          <input
            className="Input"
            required
            type="text"
            id="storage"
            defaultValue=""
            onChange={(e) => setEstoque(e.target.value)}
            value={estoque}
          />

          <label className="Label" htmlFor="username">
            Estoque Mínimo
          </label>
          <input
            className="Input"
            required
            type="text"
            id="min-storage"
            defaultValue=""
            onChange={(e) => setMinStorage(e.target.value)}
            value={estoqueMin}
          />

          <div
            style={{
              display: "flex",
              marginTop: 25,
              justifyContent: "flex-end",
            }}
          >
            {/* <Dialog.Close asChild> */}
            <button type="submit" className="Button green">
              Adicionar
            </button>
            {/* </Dialog.Close> */}
          </div>
        </form>
      </Dialog.Content>
    </Dialog.Portal>
  );
}

export default AddModal;
