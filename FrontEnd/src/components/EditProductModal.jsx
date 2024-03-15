import { useSearchParams } from "react-router-dom";
import * as Dialog from "@radix-ui/react-dialog";
import Close from '../assets/close.svg'
import axios from "axios";
import "./Edit.css";
import { useCallback, useState } from "react";
import api from "../lib/api";
import { useEffect } from "react";

function Edit({ toggleModalStatus, getProducts}) {
  const [status, setStatus] = useState();
  const [name, setName] = useState();
  const [estoque, setEstoque] = useState()
  const [estoqueMin, setEstoqueMin] = useState()
  const [searchParams, setSearchParams] = useSearchParams() 
  
  const id = searchParams.get('id')
  
  const getData = useCallback(async () => {
    const response = await axios.get(`https://localhost:8020/api/Produtos/${id}`);
    setStatus(response.data.status);
  }, [id]);

  const editProduct = async (id,{name=null,estoque,estoqueMin}) => {
    const response = await axios.patch(`https://localhost:8020/api/Produtos/${id}`,{
      
      descricao:name,
      estoqueAtual:estoque,
      estoqueMinimo:estoqueMin,
    
    });
    console.log(response.data)
  }
  
  const handleSubmit = async (e) => {
    e.preventDefault();
    await editProduct(id, { name, estoque, estoqueMin});
    
    setName(null);
    setEstoque("");
    setEstoqueMin("");
    toggleModalStatus()
    getProducts()
  };

  useEffect(() => 
  {
    console.log(id)
    getData()
  }, []);

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
        <Dialog.Title className="DialogTitle">Alterar Estoque</Dialog.Title>
        <Dialog.Description className="DialogDescription">
          Preencha os campos com os novos dados e clique em "Alterar"
        </Dialog.Description>
        <form onSubmit={handleSubmit}>
          <label className="Label" htmlFor="name">
            Descrição
          </label>
          <input
            disabled={ status && status === true}
            type="text"
            required
            className="Input"
            id="name"
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
