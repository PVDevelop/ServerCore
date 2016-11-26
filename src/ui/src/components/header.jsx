import React from "react";
import { Link } from "react-router";
import Button from "react-bootstrap/lib/Button";

export default class Header extends React.Component {
    render() {
        const isSignedIn = this.props.isSignedIn;
        let content = null;

        if (isSignedIn) {
            content = (
                <span>
                    Вы вошли в систему
                    <Button type="submit" onClick={this.props.onSignOutClicked}>
                        Выйти
                    </Button>
                </span >);
        }
        else {
            content = (
                <span>
                    Вы не вошли в систему
                    <Link to="signIn">Войти</Link>
                    <Link to="register">Создать пользователя</Link>
                </span>);
        }

        return (
            <div>
                <div>
                    Привет!
                    {content}
                </div>
            </div>
        );
    }
}