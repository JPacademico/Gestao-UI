import { useSearchParams } from "react-router-dom";
import * as Dialog from "@radix-ui/react-dialog";
import Close from "../assets/close.svg";

import "./Edit.css";
import { useCallback, useState } from "react";
import axios from "axios";
import api from "../lib/api";


function EmailModal({ toggleModalStatus, getProducts }) {
  const [email, setEmail] = useState();
  const [loading, setLoading] = useState(false)

  const [searchParams, setSearchParams] = useSearchParams();

  const id = searchParams.get("id");

  const sendEmail = useCallback(async () => {
    console.log(email);
    let response = await axios.post(`https://localhost:7286/api/Email/enviar?destinatario=${email}&idProduto=${id}`)

    console.log(response.data)
    
  });

  const changeStatus = useCallback(async (id) => {

    await getProducts();

    setSearchParams((state) => {
      if (id) {
        state.delete("id");
      }
      return state;
    });
  });

  const changeEmail =  (e) => {
    console.log(e.target.value)
    setEmail(e.target.value)
  }

  const handleSubmit = (e) => {
    e.preventDefault();

    setLoading(true)
    setTimeout(() => {
      toggleModalStatus();
      setLoading(false)
    }, 2000);
    console.log("Email enviado");
    
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
        <Dialog.Title className="DialogTitle">Enviar email</Dialog.Title>
        <Dialog.Description className="DialogDescription">
          Preencha o campo com o seu email e clique em "Enviar" para receber a
          melhor oferta!
        </Dialog.Description>
        <form onSubmit={handleSubmit}>
          <label className="Label" htmlFor="name">
            Email
          </label>
          <input
            type="email"
            required
            className="Input"
            id="email"
            onChange={(e) => changeEmail(e)}
          />

          <div
            style={{
              display: "flex",
              marginTop: 25,
              justifyContent: "flex-end",
            }}
          >
            {!loading && <button type="submit" className="Button green" onClick={() => sendEmail()}>
              Enviar
            </button>}

            {loading && <button disabled className="loadingBtn" >
              Enviando
            </button>}

            
          </div>
        </form>
      </Dialog.Content>
    </Dialog.Portal>
  );
}

export default EmailModal;
