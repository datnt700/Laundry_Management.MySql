import React from 'react';
import PropTypes from 'prop-types';

PostList.propTypes = {
  posts: PropTypes.array,
};

PostList.defaultProps = {
  posts: [],
};

function PostList(props) {
    const {posts} = props;
    return (
      <div>
        <table class="table table-hover">
          <thead>
            <tr>
              <th scope="col">Token</th>
              <th scope="col">User Name</th>
              <th scope="col">Password</th>
              <th scope="col">Create Date</th>
              <th scope="col">Action</th>
            </tr>
          </thead>
          <tbody>
            {posts ? (
              posts.map((post, index) => (
                <tr key={index} className="post-list">
                  <th>{post.Token}</th>
                  <td>{post.UserName}</td>
                  <td>{post.Password}</td>
                  <td>{post.CreateDate}</td>
                </tr>
              ))
            ) : (
              <td>{posts.Message}</td>
            )}
          </tbody>
        </table>
      </div>
    );
}

export default PostList;