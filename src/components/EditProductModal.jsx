import { useSearchParams } from "react-router-dom";
import * as Dialog from "@radix-ui/react-dialog";
import Close from '../assets/close.svg'

import "./Edit.css";
import { useCallback, useState } from "react";
import api from "../lib/api";

function Edit({  toggleModalStatus, getProducts}) {
  const [name, setName] = useState()
  const [price, setPrice] = useState()
  const [estoque, setEstoque] = useState()
  const [estoqueMin, setEstoqueMin] = useState()
  const [searchParams, setSearchParams] = useSearchParams()

  const id = searchParams.get('id')

  const editProduct = useCallback(async(id, {name, price, estoque, estoqueMin}) => {
    await api.put(`/products/${id}`, {
      name,
      price: 0,
      estoque,
      estoqueMin
    })
    
    getProducts()
    setSearchParams(state => {
      if(id){
        state.delete('id')
      }
      return state
    })
  })
  
  
  const handleSubmit = (e) => {
    e.preventDefault();
    editProduct(id, { name, price, estoque, estoqueMin});

    setName("");
    setEstoque("");
    setPrice("");
    setEstoqueMin("");

    toggleModalStatus()
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
        <Dialog.Title className="DialogTitle">Alterar Estoque</Dialog.Title>
        <Dialog.Description className="DialogDescription">
          Preencha os campos com os novos dados e clique em "Alterar"
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

          <label className="Label">
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

          <label className="Label">
            Estoque Mínimo
          </label>
          <input
            className="Input"
            required
            type="text"
            id="min-storage"
            defaultValue=""
            onChange={(e) => setEstoqueMin(e.target.value)}
            value={estoqueMin}
          />

          <div
            style={{
              display: "flex",
              marginTop: 25,
              justifyContent: "flex-end",
            }}
          >
            <button type="submit" className="Button green">
              Alterar
            </button>
          </div>
        </form>
      </Dialog.Content>
    </Dialog.Portal>
  );
}

export default Edit;
