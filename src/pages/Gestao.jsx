import { useCallback, useEffect, useState } from "react";
import Header from "../components/Header";
import * as Dialog from "@radix-ui/react-dialog";

import Xicon from "../assets/xis.svg";
import Edit from "../assets/edit.svg";
import Message from "../assets/message.svg";
import DisabledMessage from "../assets/disabledMessage.svg";

import Run from "../assets/run.svg";
import Modal from "../components/EditProductModal";

import "./Gestao.css";
import AddModal from "../components/CreateProductModal";
import api from "../lib/api";
import { price } from "../utils/priceFormater";
import { useSearchParams } from "react-router-dom";
import EmailModal from "../components/EmailModal";


function Gestao() {
  const [list, setList] = useState();
  const [modalIsOpen, setModalOpen] = useState(false);
  const [emailModalIsOpen, setEmailModalOpen] = useState(false);
  const [searchParams, setSearchParams] = useSearchParams();


  const toggleModal = () => {
    setModalOpen(modalIsOpen === true ? false : true);
  };

  const toggleEmailModal = () => {
    setEmailModalOpen(emailModalIsOpen === true ? false : true);
  };

  const setURLId = (id) => {
    setSearchParams((state) => {
      if (id) {
        state.set("id", id);
      }

      return state;
    });
  };

  const handleDeleteProduct = useCallback(async (id) => {
    await api.delete(`/products/${id}`);
    setList((state) => state.filter((product) => product.id != id));
  }, []);

  const getData = useCallback(async () => {
    const response = await api.get("/products");
    setList(response.data);
  }, [list]);

  const createNewProduct = useCallback(
    async ({ name, price, estoque, estoqueMin }) => {
      await api.post("/products", {
        name,
        price,
        estoque: estoque,
        estoqueMin: estoqueMin,
        status: 'notSended'
      });

      getData();
    }
  );

  useEffect(() => {
    getData();
  }, []);

  return (
    <>
      <Header />
      <div className="intro-box">
        <h4>Gestão de Produtos</h4>
        <Dialog.Root>
          <Dialog.Trigger asChild>
            <button className="btn-new">NOVO</button>
          </Dialog.Trigger>
          <AddModal createNewModal={createNewProduct}></AddModal>
        </Dialog.Root>
      </div>

      <div className="table-box">
        <table>
          <thead>
            <tr>
              <th>Código</th>
              <th>Descrição</th>
              <th>Preço</th>
              <th>Estoque Atual</th>
              <th>Estoque Mínimo</th>
              <th>Opções</th>
            </tr>
          </thead>

          <Dialog.Root open={modalIsOpen} onOpenChange={setModalOpen}>
            <Modal
              toggleModalStatus={toggleModal}
              getProducts={getData}
            ></Modal>
          </Dialog.Root>

          <Dialog.Root open={emailModalIsOpen} onOpenChange={setEmailModalOpen}>
            <EmailModal
              toggleModalStatus={toggleEmailModal}
              getProducts={getData}
            ></EmailModal>
          </Dialog.Root>
          
          <tbody>
            {list &&
              list.map((product) => {
                return (
                  <tr key={product.id}>
                    <td>{product.id}</td>
                    <td>{product.name}</td>
                    <td>{price.format(product.price)}</td>
                    <td>{product.estoque}</td>
                    <td>{product.estoqueMin}</td>
                    <td>
                      <div className="icons-box">
                        <button>
                          <img src={Run} alt="Iniciar Benchmarking" />
                        </button>

                        <button
                          onClick={() => {
                            toggleEmailModal();
                            setURLId(product.id);
                          }}
                          disabled={product.status !== "notSended"}
                          className="sendButton"
                        >
                          {product.status === "notSended" ? (
                            <img src={Message} alt="Enviar Email" />
                          ) : (
                            <img src={DisabledMessage} alt="Enviar Email" />
                          )}
                        </button>

                        <button
                          onClick={() => {
                            setURLId(product.id);
                            toggleModal();
                          }}
                        >
                          <img src={Edit} alt="Editar" />
                        </button>

                        <button onClick={() => handleDeleteProduct(product.id)}>
                          <img src={Xicon} alt="Apagar" />
                        </button>
                      </div>
                    </td>
                  </tr>
                );
              })}
          </tbody>
        </table>
      </div>
    </>
  );
}

export default Gestao;
