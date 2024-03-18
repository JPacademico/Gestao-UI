import React, { useEffect, useState } from "react";
import axios from "axios";
import "./Benchmark.css";
import Header from "../../components/Header";
import { price } from "../../utils/priceFormater";

export default function BenchmarkingItems() {
  const [log, setLog] = useState([]);
  const [filteredLog, setFilteredLog] = useState([]);
  const [searchId, setSearchId] = useState("");
  const [searchIdProduto, setSearchIdProduto] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const [pageNumbers, setPageNumbers] = useState([]);
  const [postsPerPage] = useState(6);

  const indexOfLastPost = currentPage * postsPerPage;
  const indexOfFirstPost = indexOfLastPost - postsPerPage;
  const currentPosts = filteredLog.slice(indexOfFirstPost, indexOfLastPost);

  const paginate = (pageNumber) => setCurrentPage(pageNumber);

  useEffect(() => {
    const pageNumbers = [];
    for (let i = 1; i <= Math.ceil(filteredLog.length / postsPerPage); i++) {
      pageNumbers.push(i);
    }
    setPageNumbers(pageNumbers);
  }, [filteredLog, postsPerPage]);

  async function readVeiculo() {
    try {
      const response = await axios.get(
        "http://3.145.53.73:8020/api/BenchmarkingItems"
      );
      setLog(response.data);
      setFilteredLog(response.data);
    } catch (error) {
      console.error("Erro ao ler logs:", error);
    }
  }

  useEffect(() => {
    readVeiculo();
  }, []);

  useEffect(() => {
    const filteredData = log.filter((data) => {
      const idMatch = data.id.toString().includes(searchId);
      const idProdutoMatch = data.idProduto
        .toString()
        .includes(searchIdProduto);
      return (
        idMatch &&
        idProdutoMatch
      );
    });
    setFilteredLog(filteredData);
  }, [
    log,
    searchId,
    searchIdProduto,
  ]);

  return (
    <>
      <Header />
      <div className="intro-box2">
        <h4>Tabela <strong>Itens do Benchmarking</strong></h4>
        <div className="filters">
        <input
            type="text"
            placeholder="Filtrar por Id"
            value={searchId}
            onChange={(e) => setSearchId(e.target.value)}
        />
        <input
            type="text"
            placeholder="Filtrar por Id Produto"
            value={searchIdProduto}
            onChange={(e) => setSearchIdProduto(e.target.value)}
        />
        </div>
        
      </div>
      
      <div className="table-box">
        <table>
          <thead>
            <tr>
              <th scope="col">IdBench</th>
              <th scope="col">Nome no MercadoLivre</th>
              <th scope="col">Link no MercadoLivre</th>
              <th scope="col">Preço no MercadoLivre</th>
              <th scope="col">Nome na MagazineLuiza</th>
              <th scope="col">Link na MagazineLuiza</th>
              <th scope="col">Preço na MagazineLuiza</th>
              <th scope="col">Economia</th>
              <th scope="col">Id Produto</th>
            </tr>
          </thead>
          <tbody>
            {currentPosts.map((data) => (
              <tr key={data.id}>
                <td>{data.id}</td>
                <td>{data.nomeLoja1}</td>
                <td><a href={data.linkLoja1} target="_blank">Clique Aqui</a></td>
                <td>{price.format(data.precoLoja1)}</td>
                <td>{data.nomeLoja2}</td>
                <td><a href={data.linkLoja2} target="_blank">Clique Aqui</a></td>
                <td>{price.format(data.precoLoja2)}</td>
                <td>{price.format(data.economia)}</td>
                <td>{data.idProduto}</td>
              </tr>
            ))}
          </tbody>
        </table>
        <nav aria-label="Page navigation example">
          <ul className="pagination">
            <li className={`page-item ${currentPage === 1 ? 'disabled' : ''}`}>
              <a className="page-link" href="#" aria-label="Previous" onClick={() => paginate(currentPage - 1)}>
                <span aria-hidden="true">&laquo;</span>
              </a>
            </li>
            {[...Array(Math.ceil(filteredLog.length / postsPerPage)).keys()].map((number) => (
              <li key={number + 1} className={`page-item ${currentPage === number + 1 ? 'active' : ''}`}>
                <a onClick={() => paginate(number + 1)} href="#" className="page-link">
                  {number + 1}
                </a>
              </li>
            ))}
            <li className={`page-item ${currentPage === Math.ceil(filteredLog.length / postsPerPage) ? 'disabled' : ''}`}>
              <a className="page-link" href="#" aria-label="Next" onClick={() => paginate(currentPage + 1)}>
                <span aria-hidden="true">&raquo;</span>
              </a>
            </li>
          </ul>
        </nav>
      </div>
    </>
  );
}
