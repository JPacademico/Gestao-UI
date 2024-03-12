import { useSearchParams } from "react-router-dom";
import * as Dialog from "@radix-ui/react-dialog";
import Close from "../assets/close.svg";

import "./Edit.css";
import { useCallback, useState } from "react";
import api from "../lib/api";


function EmailModal({ toggleModalStatus, getProducts }) {
  const [email, setEmail] = useState();
  const [loading, setLoading] = useState(false)

  const [searchParams, setSearchParams] = useSearchParams();

  const id = searchParams.get("id");

  const sendEmail = useCallback(async ({ email }) => {
    // await api.put(`/email/${email}`, {
    //     email
    // }
    
  });

  const changeStatus = useCallback(async (id) => {
    await api.patch(`/products/${id}`, {
      status: "sended",
    });

    await getProducts();

    setSearchParams((state) => {
      if (id) {
        state.delete("id");
      }
      return state;
    });
  });

  const handleSubmit = (e) => {
    e.preventDefault();
    try {
      sendEmail({ email });
      changeStatus(id);
    } catch (error) {
      console.log("Erick tomou gaia");
    }

    setEmail("");
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
            onChange={(e) => setEmail(e.target.value)}
            value={email}
          />

          <div
            style={{
              display: "flex",
              marginTop: 25,
              justifyContent: "flex-end",
            }}
          >
            {!loading && <button type="submit" className="Button green">
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
