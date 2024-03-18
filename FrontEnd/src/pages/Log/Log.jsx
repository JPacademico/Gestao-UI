import React, { useEffect, useState } from "react";
import axios from "axios";
import "./Log.css";
import Header from "../../components/Header";

export default function Logs() {
  const [log, setLog] = useState([]);
  const [filteredLog, setFilteredLog] = useState([]);
  const [searchEtapa, setSearchEtapa] = useState("");
  const [searchStatus, setSearchStatus] = useState("");
  const [searchIdProduto, setSearchIdProduto] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const [postsPerPage, setPostsPerPage] = useState(12);

  async function readLogs() {
    try {
      const response = await axios.get("http://3.145.53.73:8020/api/Logs");
      setLog(response.data);
      setFilteredLog(response.data);
    } catch (error) {
      console.error("Erro ao buscar logs:", error);
    }
  }

  useEffect(() => {
    readLogs();
  }, []);

  const handleFilterChange = (event) => {
    const { name, value } = event.target;
    switch (name) {
      case "etapa":
        setSearchEtapa(value);
        break;
      case "status":
        setSearchStatus(value);
        break;
      case "idProduto":
        setSearchIdProduto(value);
        break;
      default:
        break;
    }
  };

  useEffect(() => {
    if (log) {
      const filteredData = log.filter((data) => {
        const etapaMatch = data.etapa.toLowerCase().includes(searchEtapa.toLowerCase());
        const statusMatch = data.informacaoLog.toLowerCase().includes(searchStatus.toLowerCase());
        const idProdutoMatch = searchIdProduto === "" || data.idProduto.toString().includes(searchIdProduto);
        return etapaMatch && statusMatch && idProdutoMatch;
      });
      setFilteredLog(filteredData);
    }
  }, [log, searchEtapa, searchStatus, searchIdProduto]);

  const indexOfLastPost = currentPage * postsPerPage;
  const indexOfFirstPost = indexOfLastPost - postsPerPage;
  const currentPosts = filteredLog.slice(indexOfFirstPost, indexOfLastPost);

  const paginate = (pageNumber) => setCurrentPage(pageNumber);

  return (
    <>
      <Header />
      <div className="intro-box2">
        <h4>Tabela <strong>Logs</strong></h4>
        <div className="filters">
          <input
            type="text"
            name="etapa"
            placeholder="Filtrar por Etapa"
            value={searchEtapa}
            onChange={handleFilterChange}
          />
          <input
            type="text"
            name="status"
            placeholder="Filtrar por Status"
            value={searchStatus}
            onChange={handleFilterChange}
          />
          <input
            type="text"
            name="idProduto"
            placeholder="Filtrar por Id Produto"
            value={searchIdProduto}
            onChange={handleFilterChange}
          />
        </div>
      </div>
      <div className="table-box">
        <table>
          <thead>
            <tr>
              <th scope="col">IdLog</th>
              <th scope="col">Usuario Robo</th>
              <th scope="col">Data e Hora</th>
              <th scope="col">Etapa</th>
              <th scope="col">Status</th>
              <th scope="col">Id Produto</th>
            </tr>
          </thead>
          <tbody>
            {currentPosts.map((data) => (
              <tr key={data.idLog}>
                <td>{data.codigoRobo}</td>
                <td>{data.usuarioRobo}</td>
                <td>{data.datetime}</td>
                <td>{data.etapa}</td>
                <td>{data.informacaoLog}</td>
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
