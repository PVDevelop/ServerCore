import React from "react";
import { Link } from "react-router";

export default class Home extends React.Component {
    render() {
        return (
            <div>
                <ul>
                    <li>
                        <Link to="register">Создать пользователя</Link>
                    </li>
                    <li>
                        <Link to="signin">Войти в систему</Link>
                    </li>
                </ul>
            </div>
        );
    }
}