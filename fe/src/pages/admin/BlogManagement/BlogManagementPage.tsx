import BlogStats from './components/BlogStats'
import BlogTable from './components/BlogTable'

const BlogManagementPage = () => {
  return (
    <div className='space-y-8 p-6'>
      {/* Thống kê tổng quan */}
      <BlogStats />

      {/* Bảng quản lý blog */}
      <BlogTable />
    </div>
  )
}

export default BlogManagementPage
